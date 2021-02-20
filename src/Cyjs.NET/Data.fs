namespace Cyjs.NET

open DynamicObj

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

