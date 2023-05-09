namespace Cytoscape.NET.CytoscapeModel

open DynamicObj

type Data() =
    inherit DynamicObj ()
   
    static member Init 
        (
            Id     : string,
            ?Source : string,
            ?Target : string
        ) =    
            Data()
            |> Data.Style
                (
                    Id = Id,
                    ?Source = Source,
                    ?Target = Target
                )

    // Applies updates to Data()
    static member Style
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

