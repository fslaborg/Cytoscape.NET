namespace Cyjs.NET

open DynamicObj

type Style() =
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
                    Id,
                    Source,
                    Target
                )

    // Applies updates to Style()
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