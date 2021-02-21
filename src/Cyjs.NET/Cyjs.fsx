#r "nuget: DynamicObj, 0.0.1"
#r "nuget: Newtonsoft.Json, 13.0.1-beta1"

open Newtonsoft.Json
open DynamicObj


// type Data(id:string) =
//     inherit DynamicObj ()
//     member val ``id`` = id with get,set

type Data() =
    inherit DynamicObj ()
   
    static member node 
        (
            Id : string
        ) =    
            Data()
            |> Data.update
                (
                    Id
                )

    static member edge 
        (
            Id     : string,
            Source : string,
            Target : string
        ) =    
            Data()
            |> Data.update
                (
                    Id,
                    Source,
                    Target
                )

    // Applies updates to Data()
    static member update
        (    
            Id,
            ?Source,
            ?Target

        ) =
            (fun (data:Data) -> 

                Id        |> DynObj.setValue data "id"
                Source    |> DynObj.setValueOpt data "source"
                Target    |> DynObj.setValueOpt data "target"
                
                // out ->
                data
            )






type Element() =
    inherit DynamicObj ()

    /// Init Element()
    static member init
        (    
            ?Group    ,
            ?Data       : Data,
            ?Scratch     ,
            // the model position of the node (optional on init, mandatory after)
            ?Position   ,
            // whether the element is selected (default false)
            ?Selected   : bool,
            // whether the selection state is mutable (default true)
            ?Selectable : bool,
            // when locked a node's position is immutable (default false)
            ?Locked     : bool,
            // whether the node can be grabbed and moved by the user
            ?Grabbable  : bool,
            // whether dragging the node causes panning instead of grabbing
            ?Pannable   : bool,
            ?Classes    : List<string>
        ) =    
            Element()
            |> Element.update
                (
                    ?Group       = Group     ,
                    ?Data        = Data      ,
                    ?Scratch     = Scratch   ,
                    ?Position    = Position  ,
                    ?Selected    = Selected  ,
                    ?Selectable  = Selectable,
                    ?Locked      = Locked    ,
                    ?Grabbable   = Grabbable ,
                    ?Pannable    = Pannable  ,
                    ?Classes     = Classes 
                )


    // Applies the styles to Element()
    static member update
        (    
            ?Group     ,
            ?Data      ,
            ?Scratch   ,
            ?Position  ,
            ?Selected   : bool,
            ?Selectable : bool,
            ?Locked     : bool,
            ?Grabbable  : bool,
            ?Pannable   : bool,
            ?Classes    : List<string>
        ) =
            (fun (element:Element) -> 

                Group        |> DynObj.setValueOpt element "group" 
                Data         |> DynObj.setValueOpt element "data" 
                Scratch      |> DynObj.setValueOpt element "scratch" 
                Position     |> DynObj.setValueOpt element "position" 
                Selected     |> DynObj.setValueOpt element "selected" 
                Selectable   |> DynObj.setValueOpt element "selectable" 
                Locked       |> DynObj.setValueOpt element "locked" 
                Grabbable    |> DynObj.setValueOpt element "grabbable" 
                Pannable     |> DynObj.setValueOpt element "pannable" 
                Classes      |> DynObj.setValueOpt element "classes" 
                        
                // out ->
                element
            )


type PlainJsonString(str:string) = 
    member val Value = str with get,set

type PlainJsonStringConverter() =
    inherit JsonConverter()

    override __.CanConvert(objectType) =
        typeof<PlainJsonString> = objectType
    
    override __.ReadJson(reader, objectType, existingValue, serializer) =
        reader.Value

    override __.WriteJson(writer, value, serializer) =
        let v = value :?> PlainJsonString
        writer.WriteRawValue(string v.Value)
        

type Cytoscape() = 
    inherit DynamicObj ()

    let tmpContainer  = "document.getElementById('cy')" |> PlainJsonString
    let tmpElements  = new System.Collections.Generic.List<DynamicObj>()
    let tmpStyle     = new System.Collections.Generic.List<DynamicObj>()
    let tmpLayout    = new System.Collections.Generic.List<DynamicObj>()

    // member this.Add (item:#DynamicObj) = 
    //     tmpContainer.Add(item) 

    member this.AddElement (element:#Element) = 
        tmpElements.Add(element) 

    member this.AddStyle (style:#DynamicObj) = 
        tmpStyle.Add(style) 

    member this.AddLayout (item:#DynamicObj) = 
        tmpLayout.Add(item) 

    //[<JsonConverter(typeof<PlainJsonStringConverter>)>]
    member val ``container`` = tmpContainer //with get,set 
    member val ``elements``  = tmpElements  with get,set 
    member val ``style``     = tmpStyle     with get,set 
    member val ``layout``    = tmpLayout    with get,set 



open System
open System.IO
open Newtonsoft.Json
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

/// HTML template for Cytoscape
module HTML =
    let doc =
        """
<!DOCTYPE html>
<html>
<head>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.18.0/cytoscape.min.js"></script>
</head>

<style>
    #cy {
        width: 100%;
        height: 100%;
        position: absolute;
        top: 0px;
        left: 0px;
    }
</style>

<body>
    <div id="cy"></div>
    <script>
      var cy = cytoscape(
        [GRAPH]
        );
    </script>
</body>
</html>
"""

    let toCytoHTML (cy:Cytoscape) =
        
        let json = JsonConvert.SerializeObject(cy,PlainJsonStringConverter())
            //cy
            //|> JsonConvert.SerializeObject

        
        let html =
            doc
                //.Replace("style=\"width: [WIDTH]px; height: [HEIGHT]px;\"","style=\"width: 600px; height: 600px;\"")
                .Replace("[GRAPH]", json)
               
        html

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
        let html = toCytoHTML cy
        let tempPath = Path.GetTempPath()
        let file = sprintf "%s.html" guid
        let path = Path.Combine(tempPath, file)
        File.WriteAllText(path, html)
        path |> openOsSpecificFile



//Newtonsoft.Json.JsonConvert.SerializeObject cy //Formatting.Indented)
let n1 = Element.init(Data = (Data.node "n1"))
let n2 = Element.init(Data = (Data.node "n2"))
let e  = Element.init(Data = (Data.edge ("e", "n1", "n2")))

let graph =
    let cy = Cytoscape()
    cy.AddElement(n1)
    cy.AddElement(n2)
    cy.AddElement(e)
    cy

HTML.show graph




