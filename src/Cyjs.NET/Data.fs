namespace Cyjs.NET.CytoscapeModel

open DynamicObj


type Data() =
    inherit DynamicObj ()
   
    static member init 
        (
            Id     : string,
            ?Source : string,
            ?Target : string
        ) =    
            Data()
            |> Data.style
                (
                    Id = Id,
                    ?Source = Source,
                    ?Target = Target
                )

    // Applies updates to Data()
    static member style
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

