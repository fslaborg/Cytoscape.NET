namespace Cyjs.NET.CytoscapeModel

open DynamicObj
open Cyjs.NET

type Layout() =
    inherit DynamicObj ()
   
    static member Init 
        (
            name
        ) =    
            Layout()
            |> Layout.Style
                (
                    name=name
                )

    // Applies updates to Style()
    static member Style
        (    
            name : string

        ) =
            (fun (layout:Layout) -> 

                name        |> DynObj.setValue layout "name"
                //Target    |> DynObj.setValueOpt data "target"
                
                // out ->
                layout
            )

