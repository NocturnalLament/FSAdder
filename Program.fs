// For more information see https://aka.ms/fsharp-console-apps
open System
open System.IO
open System.Xml.Linq
open System.Linq

// Appends the required line to the xml
let ParseXML(fsprojFiles: string array, documentToAdd: string) =
    // Gets the first file returned from the getfiles method, hopefully the only file there.
    let fsProjPath = fsprojFiles.[0]
    // Loads said document.
    let doc = XDocument.Load(fsProjPath)
    // Looks up the itemgroup and finds it.
    let itemGroup = 
        doc.Descendants(XName.Get("ItemGroup"))
        // Returns the first itemgroup element found with a compile, if any.
        |> Seq.tryFind (fun group -> group.Elements(XName.Get("Compile")).Any())
    // Unwraps the option
    match itemGroup with
    | Some group ->
        // Appends the requested item to the project file.
        let newElement = XElement(XName.Get("Compile"), XAttribute(XName.Get("Include"), documentToAdd))
        group.AddFirst(newElement)
        doc.Save(fsProjPath)
    | None ->
        printfn "No Item was added. Try again."

[<EntryPoint>]
let main argv =
    // First element is the program name. Don't need that.
    let docuName = argv.[0] 
    let currentDir = Directory.GetCurrentDirectory()
    let fsproj = Directory.GetFiles(currentDir, "*.fsproj")
    match fsproj.Length with
    | 0 ->
        printfn "You don't have any .fsproj files in this directory."
        0
    | 1 ->
        ParseXML(fsproj, docuName)
        printfn "Done."
        0
    | _ -> 1
    |> ignore
    0
