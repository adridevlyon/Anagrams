module AnagramFinder

open System.Collections.Generic

// types
type wordKey = {crc:int; letters:string list; length:int}
type word = {Original:string; Key:string}
type anagrams = {HasMoreThanOne:bool; Words:string list}

// utils
let joinStringList sep strlist =
    let mutable res = ""
    Seq.iter (fun str -> res <- res + str + sep) strlist
    res
    
let readLines filePath = System.IO.File.ReadLines(filePath)

// create word
let addHash crc c = crc + c.GetHashCode();

let addLetterIfneeded letters c =
    match List.exists (fun e -> e = c) letters with
    | true -> letters
    | false -> c::letters

let handleChar outputWordKey c =
    {outputWordKey with letters = addLetterIfneeded outputWordKey.letters (c.ToString()); crc=addHash outputWordKey.crc c}

let convertEntryToWord str = 
    let mutable outputWordKey = {crc=0; letters=[]; length=String.length str}
    Seq.iter (fun c -> outputWordKey <- handleChar outputWordKey c) str
    {Original=str; Key=System.String.Format("{0}-{1}-{2}", outputWordKey.crc, outputWordKey.length, (joinStringList "" (List.sort outputWordKey.letters)))}

// handle words
let handleWord (words:Dictionary<string, anagrams>) (word:word) =
    match words.ContainsKey(word.Key) with
    | true -> words.[word.Key] <- {words.[word.Key] with HasMoreThanOne=true; Words=word.Original::words.[word.Key].Words}
    | false -> words.Add(word.Key, {HasMoreThanOne=false; Words=[word.Original]})

let getAnagramsAsString anagram =
    match anagram.HasMoreThanOne with
    | true -> anagram.Words |> joinStringList " "
    | false -> ""

let getAnagrams anags =
    let mutable anagrams = []
    for anagram in anags do
        match getAnagramsAsString anagram with
        | "" -> ()
        | anag -> anagrams <- anag::anagrams
    anagrams

let getAnagramsFromRawWords rawWords =
    let resultWords = Dictionary<string, anagrams>()
    Seq.iter (fun str -> handleWord resultWords (convertEntryToWord str)) rawWords
    getAnagrams resultWords.Values

// print all anagrams
let printAnagrams filePath =
    let stopWatch = System.Diagnostics.Stopwatch.StartNew()
    let inputWords = readLines filePath
    let toPrint = getAnagramsFromRawWords inputWords
    stopWatch.Stop()
    Seq.iter (printfn "%s") toPrint
    printfn "time to calculate : %f ms" stopWatch.Elapsed.TotalMilliseconds