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







//Newtonsoft.Json.JsonConvert.SerializeObject cy //Formatting.Indented)

