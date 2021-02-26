namespace Cyjs.NET.CytoscapeModel

open DynamicObj

type Position() =
    inherit DynamicObj ()

    static member Init 
        (
            x : int,
            y : int
        ) =    
            let pos = Position()
            x        |> DynObj.setValue pos "x"
            y        |> DynObj.setValue pos "y"
            
            // out ->
            pos
            


type Zoom() =
    inherit DynamicObj ()
   
    static member Init 
        (
            ?Level            : float,
            ?Position         : int*int,
            ?RenderedPosition : int*int,
            ?ZoomingEnabled   : bool
        ) =    
            Zoom()
            |> Zoom.Update
                (
                    ?Level = Level,
                    ?Position = Position,
                    ?RenderedPosition = RenderedPosition,
                    ?ZoomingEnabled = ZoomingEnabled
                )

    // Applies updates to Zoom()
    static member Update
        (    
            ?Level,
            ?Position,
            ?RenderedPosition,
            ?ZoomingEnabled

        ) =
            (fun (zoom:Zoom) -> 

                Level            |> DynObj.setValue zoom "level"
                Position         |> DynObj.setValueOpt zoom "position"
                RenderedPosition |> DynObj.setValueOpt zoom "renderedPosition"
                // Is parsed out and set in javascript (see HTML module)
                ZoomingEnabled   |> DynObj.setValueOpt zoom "zoomingEnabled"
                // out ->
                zoom
            )