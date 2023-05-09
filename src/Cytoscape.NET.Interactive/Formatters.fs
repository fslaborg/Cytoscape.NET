namespace Cytoscape.NET.Interactive

open Cytoscape.NET
open Cytoscape.NET.CytoscapeModel

module Formatters = 
    
    /// Converts a Cytoscape type to it's HTML representation to show in a notebook environment.
    let toInteractiveHTML (graph:Cytoscape): string = 
        graph
        |> HTML.toGraphHTML(
            DisplayOpts = DisplayOptions.init(CytoscapeJSReference = CytoscapeJSReference.Require $"https://cdnjs.cloudflare.com/ajax/libs/cytoscape/{Globals.CYTOSCAPEJS_VERSION}/cytoscape.min")
        )