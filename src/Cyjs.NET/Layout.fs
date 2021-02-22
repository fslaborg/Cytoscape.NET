namespace Cyjs.NET.CytoscapeModel

open DynamicObj
open Cyjs.NET

type Layout() =
    inherit DynamicObj ()
   
    static member init 
        (
            Name
        ) =    
            Layout()
            |> Layout.style
                (
                    Name=Name
                )

    // Applies updates to Style()
    static member style
        (    
            Name : string

        ) =
            (fun (layout:Layout) -> 

                Name        |> DynObj.setValue layout "name"
                //Target    |> DynObj.setValueOpt data "target"
                
                // out ->
                layout
            )

