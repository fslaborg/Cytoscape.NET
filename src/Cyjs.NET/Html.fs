namespace Cyjs.NET

open System
open System.IO
open Newtonsoft.Json
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

open Cyjs.NET.CytoscapeModel

/// HTML template for Cytoscape
module HTML =
    
    let doc =
        let newScript = System.Text.StringBuilder()
        newScript.AppendLine("""<!DOCTYPE html>""") |> ignore
        newScript.AppendLine("<html>") |> ignore
        newScript.AppendLine("<head>") |> ignore
        newScript.AppendLine("""<script src="https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.18.0/cytoscape.min.js"></script>""") |> ignore
        newScript.AppendLine("</head>") |> ignore
        newScript.AppendLine("<body> [GRAPH] </body>") |> ignore
        newScript.AppendLine("""</html>""") |> ignore
        newScript.ToString() 

    let graphDoc =
        let newScript = System.Text.StringBuilder()
        newScript.AppendLine("""<style>#[ID] [CANVAS] </style>""") |> ignore
        //newScript.AppendLine("""<style>#[ID] { width: [WIDTH]px; height: [HEIGHT]px; display: block }</style>""") |> ignore
        //newScript.AppendLine("""<style>#[ID] { width: 100%; height: 100%; position: absolute; top: 0px; left: 0px; }</style>""") |> ignore
        newScript.AppendLine("""<div id="[ID]"></div>""") |> ignore
        newScript.AppendLine("<script type=\"text/javascript\">") |> ignore
        newScript.AppendLine(@"
            var renderCyjs_[SCRIPTID] = function() {
            var fsharpCyjsRequire = requirejs.config({context:'fsharp-cyjs',paths:{cyjs:'https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.18.0/cytoscape.min'}}) || require;
            fsharpCyjsRequire(['cyjs'], function(Cyjs) {")  |> ignore
        newScript.AppendLine(@"
            var graphdata = [GRAPHDATA]
            var cy = cytoscape( graphdata );
            cy.userZoomingEnabled( [ZOOMING] );
            ")  |> ignore
        newScript.AppendLine("""});
            };
            if ((typeof(requirejs) !==  typeof(Function)) || (typeof(requirejs.config) !== typeof(Function))) {
                var script = document.createElement("script");
                script.setAttribute("src", "https://cdnjs.cloudflare.com/ajax/libs/require.js/2.3.6/require.min.js");
                script.onload = function(){
                    renderCyjs_[SCRIPTID]();
                };
                document.getElementsByTagName("head")[0].appendChild(script);
            }
            else {
                renderCyjs_[SCRIPTID]();
            }""") |> ignore
        newScript.AppendLine("</script>") |> ignore
        newScript.ToString() 

    /// Converts a CyGraph to it HTML representation. The div layer has a default size of 600 if not specified otherwise.
    let toCytoHTML (cy:Cytoscape) =
        let guid = Guid.NewGuid().ToString()
        let id   = sprintf "e%s" <| Guid.NewGuid().ToString().Replace("-","").Substring(0,10)
        cy.container <- PlainJsonString id

        let userZoomingEnabled =
            match cy.TryGetTypedValue<Zoom> "zoom"  with
            | Some z -> 
                match z.TryGetTypedValue<bool> "zoomingEnabled" with
                | Some t -> t
                | None -> false
            | None -> false
            |> string 
            |> fun s -> s.ToLower()

        let strCANVAS = // DynamicObj.DynObj.tryGetValue cy "Dims" //tryGetLayoutSize gChart
            match cy.TryGetTypedValue<Canvas> "Canvas"  with
            |Some c -> c
            |None -> Canvas.InitDefault()
            //|> fun c -> c?display <-  "block" ; c
            |> fun c -> 
                c.GetProperties(true)
                |> Seq.map (fun k -> sprintf "%s: %O" k.Key k.Value)
                |> String.concat "; "
                |> sprintf "{ %s }" 
                    
        DynamicObj.DynObj.remove cy "Canvas"
        let jsonGraph = JsonConvert.SerializeObject (cy,PlainJsonStringConverter())

        let html =
            graphDoc
                .Replace("[CANVAS]", strCANVAS)
                .Replace("[ID]", id)
                .Replace("[ZOOMING]", userZoomingEnabled)
                .Replace("[SCRIPTID]", guid.Replace("-",""))
                .Replace("[GRAPHDATA]", jsonGraph)                
        html

    /// Converts a CyGraph to it HTML representation and embeds it into a html page.
    let toEmbeddedHTML (cy:Cytoscape) =
        let graph =
            toCytoHTML cy
        doc
            .Replace("[GRAPH]", graph)
            //.Replace("[DESCRIPTION]", "")


    ///Choose process to open plots with depending on OS. Thanks to @zyzhu for hinting at a solution (https://github.com/plotly/Plotly.NET/issues/31)
    let internal openOsSpecificFile path =
        if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then
            let psi = System.Diagnostics.ProcessStartInfo(FileName = path, UseShellExecute = true)
            System.Diagnostics.Process.Start(psi) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then
            System.Diagnostics.Process.Start("xdg-open", path) |> ignore
        elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then
            System.Diagnostics.Process.Start("open", path) |> ignore
        else
            invalidOp "Not supported OS platform"


    let show (cy:Cytoscape) = 
        let guid = Guid.NewGuid().ToString()
        let html = toEmbeddedHTML cy
        let tempPath = Path.GetTempPath()
        let file = sprintf "%s.html" guid
        let path = Path.Combine(tempPath, file)
        File.WriteAllText(path, html)
        path |> openOsSpecificFile
