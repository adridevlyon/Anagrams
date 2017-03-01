module AnagramFinderTests

open System.Collections.Generic
open AnagramFinder

let entry = "test"
let entryConverted = convertEntryToWord entry
let addExistingLetter = addLetterIfneeded ['a';'b'] 'a'
let addLetter = addLetterIfneeded ['a';'b'] 'c'

let words = Dictionary<string, anagrams>()
handleWord words (convertEntryToWord "test")
handleWord words (convertEntryToWord "toto")
handleWord words (convertEntryToWord "otot")
handleWord words (convertEntryToWord "sett")
words |> ignore
