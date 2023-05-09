module internal InternalUtils

open System.Reflection
open System.IO

let getFullCytoscapeJS () =
    let assembly = Assembly.GetExecutingAssembly()
    let resourceName = "Cyjs.NET.cytoscape.min.js"
    use stream = assembly.GetManifestResourceStream(resourceName)
    use reader = new StreamReader(stream)
    reader.ReadToEnd()