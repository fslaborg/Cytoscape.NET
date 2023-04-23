namespace Cyjs.NET

open System
open System.IO
open Newtonsoft.Json
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

open Cyjs.NET.CytoscapeModel
open Giraffe.ViewEngine


/// HTML template for Cytoscape
type HTML =
    
    static member CreateGraphScript
        (
            graphData: string,
            zooming: string,
            cytoscapeReference: CytoscapeJSReference
        ) =
        match cytoscapeReference with
        | Require r ->
            script
                [ _type "text/javascript" ]
                [
                    rawText (
                        Globals.REQUIREJS_SCRIPT_TEMPLATE
                            .Replace("[REQUIRE_SRC]", r)
                            .Replace("[GRAPHDATA]", graphData)
                            .Replace("[ZOOMING]", zooming)
                    )
                ]
        | _ ->
            script
                [ _type "text/javascript" ]
                [
                    rawText (
                        Globals.SCRIPT_TEMPLATE
                            .Replace("[GRAPHDATA]", graphData)
                            .Replace("[ZOOMING]", zooming)
                    )
                ]


    static member Doc(
        graphHTML: XmlNode list, 
        cytoscapeReference: CytoscapeJSReference, 
        ?AdditionalHeadTags
    ) =
        let additionalHeadTags =
            defaultArg AdditionalHeadTags []

        let cytoscapeScriptRef =
            match cytoscapeReference with
            | CDN cdn -> script [ _src cdn ] []
            | Full ->
                script
                    [ _type "text/javascript" ]
                    [
                        rawText (InternalUtils.getFullCytoscapeJS ())
                    ]
            | NoReference
            | Require _ -> rawText ""

        html
            []
            [
                head
                    []
                    [
                        cytoscapeScriptRef
                        yield! additionalHeadTags
                    ]
                body [] [ yield! graphHTML]
            ]

    static member CreateGraphHTML
        (
            graphData: string,
            zooming: string,
            divId: string,
            cytoscapeReference: CytoscapeJSReference,
            ?Width: int,
            ?Height: int
        ) =
        let width, height = 
            Width |> Option.defaultValue Defaults.DefaultWidth,
            Height |> Option.defaultValue Defaults.DefaultHeight

        let graphScript =
            HTML.CreateGraphScript(
                graphData = graphData,
                zooming = zooming,
                cytoscapeReference = cytoscapeReference
            )

        [
            div
                [ _id divId; _style $"width:{width}px; height:{height}px"]
                [
                    comment "Plotly chart will be drawn inside this DIV"
                ]
            graphScript
        ]

    /// Converts a CyGraph to it HTML representation. The div layer has a default size of 600 if not specified otherwise.
    static member toGraphHTMLNodes (
        ?DisplayOpts: DisplayOptions
    ) =
        fun (cy:Cytoscape) -> 

            let displayOptions = defaultArg DisplayOpts Defaults.DefaultDisplayOptions
            let cytoscapeReference = displayOptions |> DisplayOptions.getCytoscapeReference

            let guid = Guid.NewGuid().ToString()
            let id   = sprintf "e%s" <| Guid.NewGuid().ToString().Replace("-","").Substring(0,10)
        
            cy.container <- PlainJsonString id
            DynamicObj.DynObj.remove cy "Canvas" // no need for canvas, should remove it completely in the future

            let userZoomingEnabled =
                match cy.TryGetTypedValue<Zoom> "zoom"  with
                | Some z -> 
                    match z.TryGetTypedValue<bool> "zoomingEnabled" with
                    | Some t -> t
                    | None -> false
                | None -> false
                |> string 
                |> fun s -> s.ToLower()
                    
            let jsonGraph = JsonConvert.SerializeObject (cy,PlainJsonStringConverter())

            HTML.CreateGraphHTML(
                graphData = jsonGraph,
                zooming = userZoomingEnabled,
                divId = id,
                cytoscapeReference = cytoscapeReference
            )

    static member toGraphHTML(
        ?DisplayOpts: DisplayOptions
    ) =
        fun (cy:Cytoscape) -> 
            cy
            |> HTML.toGraphHTMLNodes(?DisplayOpts = DisplayOpts)
            |> RenderView.AsString.htmlNodes

    /// Converts a CyGraph to it HTML representation and embeds it into a html page.
    static member toEmbeddedHTML (
        ?DisplayOpts: DisplayOptions
    ) =
        fun (cy:Cytoscape) ->
            let displayOptions = defaultArg DisplayOpts Defaults.DefaultDisplayOptions
            let cytoscapeReference = DisplayOptions.getCytoscapeReference displayOptions
            HTML.Doc(
                graphHTML = (HTML.toGraphHTMLNodes(DisplayOpts = displayOptions) cy),
                cytoscapeReference = cytoscapeReference
            )
            |> RenderView.AsString.htmlDocument


    ///Choose process to open plots with depending on OS. Thanks to @zyzhu for hinting at a solution (https://github.com/plotly/Plotly.NET/issues/31)
    static member internal openOsSpecificFile path =
        if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then
            let psi = System.Diagnostics.ProcessStartInfo(FileName = path, UseShellExecute = true)
            System.Diagnostics.Process.Start(psi) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then
            System.Diagnostics.Process.Start("xdg-open", path) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then
            System.Diagnostics.Process.Start("open", path) |> ignore
        else
            invalidOp "Not supported OS platform"


    static member show (cy:Cytoscape, ?DisplayOpts: DisplayOptions) = 
        let guid = Guid.NewGuid().ToString()
        let html = HTML.toEmbeddedHTML(?DisplayOpts = DisplayOpts) cy
        let tempPath = Path.GetTempPath()
        let file = sprintf "%s.html" guid
        let path = Path.Combine(tempPath, file)
        File.WriteAllText(path, html)
        path |> HTML.openOsSpecificFile
