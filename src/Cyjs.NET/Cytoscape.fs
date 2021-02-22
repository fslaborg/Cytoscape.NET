namespace Cyjs.NET.CytoscapeModel

open DynamicObj
open Cyjs.NET

    
type Cytoscape() = 
    inherit DynamicObj ()

    //let tmpContainer  = "document.getElementById('cy')" |> PlainJsonString
    let mutable tmpId = "cy" |> PlainJsonString
    let tmpElements   = new System.Collections.Generic.List<DynamicObj>()
    let tmpStyle      = new System.Collections.Generic.List<DynamicObj>()
    let tmpLayout     = Layout.init("random")//new System.Collections.Generic.List<DynamicObj>() 

    member this.AddElement (element:#Element) = 
        tmpElements.Add(element) 

    member this.AddStyle (style:#DynamicObj) = 
        tmpStyle.Add(style) 

    // member this.AddLayout (item:#DynamicObj) = 
    //     tmpLayout.Add(item) 


    member this.``container`` with get () = PlainJsonString (sprintf "document.getElementById('%s')" tmpId.Value)
                              and set (id:PlainJsonString) = tmpId <- id 
    member val ``elements``  = tmpElements  with get,set 
    member val ``style``     = tmpStyle     with get,set 
    member val ``layout``    = tmpLayout    with get,set 

