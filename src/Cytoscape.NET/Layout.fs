namespace Cytoscape.NET

open DynamicObj


/// Layout type inherits from dynamic object
type Layout (name) =
    inherit DynamicObj ()

    member val ``name`` = name with get,set



[<CompilationRepresentationAttribute(CompilationRepresentationFlags.ModuleSuffix)>]
module Layout = 
    

    //Layout Types

    //-------------------------------------------------------------------------------------------------------------------------------------------------

    /// initializes a layout of type "preset" applying the givin layout option function.
    /// The "preset" layout puts nodes in the positions you specify manually.
    let initPresent (applyOption:Layout->Layout) = 
        Layout("preset") |> applyOption

    /// initializes a layout of type "grid" applying the givin layout option function 
    /// The "grid" layout puts nodes in a well-spaced grid.
    let initGrid (applyOption:Layout->Layout) = 
        Layout("grid") |> applyOption

    /// initializes a layout of type "circle" applying the givin layout option function.
    /// The "circle" layout puts nodes in a circle.
    let initCircle (applyOption:Layout->Layout) = 
        Layout("circle") |> applyOption

    /// initializes a layout of type "concentric" applying the givin layout option function.
    /// The concentric layout positions nodes in concentric circles, based on a metric that you specify to segregate the nodes into levels.
    let initConcentric (applyOption:Layout->Layout) = 
        Layout("concentric") |> applyOption

    /// initializes a layout of type "breadthfirst" applying the givin layout option function.
    /// The "breadthfirst" layout puts nodes in a hierarchy, based on a breadthfirst traversal of the graph. It is best suited to trees
    /// and forests in its default top-down mode, and it is best suited to DAGs in its circle mode.
    let initBreadthfirst (applyOption:Layout->Layout) = 
        Layout("breadthfirst") |> applyOption

    /// initializes a layout of type "cose" applying the givin layout option function.
    /// The cose (Compound Spring Embedder) layout uses a physics simulation to lay out graphs.
    let initCose (applyOption:Layout->Layout) = 
        Layout("cose") |> applyOption

    /// initializes a layout of type "cose-bilkent" applying the givin layout option function.
    /// The cose-bilkent extension is an evolution of the cose algorithm that is more computationally expensive but produces near-perfect results.
    let initCoseBilkent (applyOption:Layout->Layout) = 
        Layout("cose-bilkent") |> applyOption

    /// initializes a layout of type "random" applying the givin layout option function.
    let initRandom (applyOption:Layout->Layout) = 
        Layout("random") |> applyOption


    /// Functions provide the options of the Layout objects
    type LayoutOptions() =
        
        /// Applies the generic layout options to the LayoutObjects
        static member Generic
            (    
                ?Positions,    // map of (node id) => (position obj); or function(node){ return somPos; }
                ?Zoom,         // the zoom level to set (prob want fit = false if set)
                ?Pan,          // the pan level to set (prob want fit = false if set)
                ?Fit: bool,    // whether to fit to viewport
                ?Padding: int, // padding on fit
                ?Animate: bool, // whether to transition the node positions
                ?AnimationDuration: int, // duration of animation in ms if enabled
                ?AnimationEasing, // easing of animation if enabled
                ?AnimateFilter, // a function that determines whether the node should be animated.  All nodes animated by default on animate enabled.  Non-animated nodes are positioned immediately when the layout starts
                ?AnimationThreshold : int, // The layout animates only after this many milliseconds for animate:true -> prevents flashing on fast runs)
                ?Ready, // callback on layoutready
                ?Stop, // callback on layoutstop
                ?Transform // transform a given node position. Useful for changing flow direction in discrete layouts
                
            ) =
                (fun (layout:('L :> Layout)) ->  
                    Positions         |> DynObj.setValueOpt   layout "positions"
                    Zoom              |> DynObj.setValueOpt   layout "zoom"
                    Pan               |> DynObj.setValueOpt   layout "pan"
                    Fit               |> DynObj.setValueOpt   layout "fit"  
                    Padding           |> DynObj.setValueOpt   layout "padding"
                    Animate           |> DynObj.setValueOpt   layout "animate"
                    AnimationDuration |> DynObj.setValueOpt   layout "animationDuration"
                    AnimationEasing   |> DynObj.setValueOpt   layout "animationEasing"
                    AnimateFilter     |> DynObj.setValueOpt   layout "animateFilter"
                    AnimationThreshold|> DynObj.setValueOpt   layout "animationThreshold"
                    Ready             |> DynObj.setValueOpt   layout "ready"
                    Stop              |> DynObj.setValueOpt   layout "stop"
                    Transform         |> DynObj.setValueOpt   layout "transform"                    
                    // out ->
                    layout
                ) 

       /// Applies the generic layout options to the LayoutObjects
        static member Cose
            (    
                ?Refresh : int, // Number of iterations between consecutive screen positions update
                ?BoundingBox, // Constrain layout bounds; { x1, y1, x2, y2 } or { x1, y1, w, h }
                ?NodeDimensionsIncludeLabels : bool, // Excludes the label when calculating node bounding boxes for the layout algorithm
                ?Randomize : bool, // Randomize the initial positions of the nodes (true) or use existing positions (false)
                ?ComponentSpacing : int, // Extra spacing between components in non-compound graphs
                ?NodeRepulsion, // Node repulsion (non overlapping) multiplier
                ?NodeOverlap : int, // Node repulsion (overlapping) multiplier
                ?IdealEdgeLength, // Ideal edge (non nested) length
                ?EdgeElasticity, // Divisor to compute edge forces
                ?NestingFactor: float, // Nesting factor (multiplier) to compute ideal edge length for nested edges
                ?Gravity: int, // Gravity force (constant)
                ?NumIter: int, // Maximum number of iterations to perform
                ?InitialTemp: int, // Initial temperature (maximum node displacement)
                ?CoolingFactor: float, // Cooling factor (how the temperature is reduced between consecutive iterations
                ?MinTemp : float // Lower temperature threshold (below this point the layout will end)
                
            ) =
                (fun (layout:('L :> Layout)) ->  
                    Refresh                     |> DynObj.setValueOpt   layout "refresh"
                    BoundingBox                 |> DynObj.setValueOpt   layout "boundingBox"
                    NodeDimensionsIncludeLabels |> DynObj.setValueOpt   layout "nodeDimensionsIncludeLabels"
                    Randomize                   |> DynObj.setValueOpt   layout "randomize"  
                    ComponentSpacing            |> DynObj.setValueOpt   layout "componentSpacing"
                    NodeRepulsion               |> DynObj.setValueOpt   layout "nodeRepulsion"
                    NodeOverlap                 |> DynObj.setValueOpt   layout "nodeOverlap"
                    IdealEdgeLength             |> DynObj.setValueOpt   layout "idealEdgeLength"
                    EdgeElasticity              |> DynObj.setValueOpt   layout "edgeElasticity"
                    NestingFactor               |> DynObj.setValueOpt   layout "nestingFactor"
                    Gravity                     |> DynObj.setValueOpt   layout "gravity"
                    NumIter                     |> DynObj.setValueOpt   layout "numIter"
                    InitialTemp                 |> DynObj.setValueOpt   layout "initialTemp"                    
                    CoolingFactor               |> DynObj.setValueOpt   layout "coolingFactor"
                    MinTemp                     |> DynObj.setValueOpt   layout "minTemp"
                    // out ->
                    layout
                )   