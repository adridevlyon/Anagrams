module AnagramFinder

let toto = "test"

let addHash crc c = crc + c.GetHashCode();

let iterateOverString str = 
    let mutable crc = 0
    Seq.iter (fun c -> crc <- addHash crc c) str
    crc

iterateOverString toto