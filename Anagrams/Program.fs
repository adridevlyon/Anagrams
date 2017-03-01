// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open AnagramFinder

[<EntryPoint>]
let main argv = 
    let fileName = @"C:\Users\a.marion\Documents\wordlist.txt";
    printAnagrams fileName
    0 // return an integer exit code
