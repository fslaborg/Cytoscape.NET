namespace Cyjs.NET

open DynamicObj


type Element() =
    inherit DynamicObj ()
    

    /// Init Element()
    static member init
        (    
            ?Data       : Data,
            ?Group    ,
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
                    ?Data        = Data      ,
                    ?Group       = Group     ,
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
            
            ?Data      ,
            ?Group     ,
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

                Data         |> DynObj.setValueOpt element "data" 
                Group        |> DynObj.setValueOpt element "group" 
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
