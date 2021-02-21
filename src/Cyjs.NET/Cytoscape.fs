namespace Cyjs.NET

open DynamicObj



type Cytoscape() = 
    inherit DynamicObj ()

    let tmpContainer = new System.Collections.Generic.List<DynamicObj>()
    let tmpElements  = new System.Collections.Generic.List<DynamicObj>()
    let tmpStyle     = new System.Collections.Generic.List<DynamicObj>()
    let tmpLayout    = new System.Collections.Generic.List<DynamicObj>()

    member this.Add (item:#DynamicObj) = 
        tmpContainer.Add(item) 

    member this.AddElement (element:#Element) = 
        tmpElements.Add(element) 

    member this.AddStyle (style:#DynamicObj) = 
        tmpStyle.Add(style) 

    member this.AddLayout (item:#DynamicObj) = 
        tmpLayout.Add(item) 


    member val ``container`` = tmpContainer with get,set 
    member val ``elements``  = tmpElements  with get,set 
    member val ``style``     = tmpStyle     with get,set 
    member val ``layout``    = tmpLayout    with get,set 

