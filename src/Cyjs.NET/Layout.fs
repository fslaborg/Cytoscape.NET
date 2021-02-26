namespace Cyjs.NET.CytoscapeModel

open DynamicObj
open Cyjs.NET

type Layout() =
    inherit DynamicObj ()
   
    static member Init 
        (
            Name
        ) =    
            Layout()
            |> Layout.Style
                (
                    Name=Name
                )

    // Applies updates to Style()
    static member Style
        (    
            Name : string

        ) =
            (fun (layout:Layout) -> 

                Name        |> DynObj.setValue layout "name"
                //Target    |> DynObj.setValueOpt data "target"
                
                // out ->
                layout
            )

