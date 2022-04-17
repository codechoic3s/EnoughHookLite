const json1 = require('./jsonraw.json');
const json2 = require('./signatures.json');
const fs = require('fs');

function main(){
    json2.Components.forEach(el2 => {
        json1.signatures.forEach(el1 => {
            if(el2.Name == el1.name)
            {
                if(el1.offsets==undefined) el2.Offsets = [];
                else el2.Offsets = el1.offsets;
                el2.Relative = el1.relative;
                el2.Extra = el1.extra;
                el2.Module = el1.module;
                el2.Signature = stringToHex(el1.pattern);
            }
        });
    });

    fs.writeFile("doned.json", JSON.stringify(json2, null, 2), 'utf8', function (err) {
        if (err) {
            console.log("An error occured while writing JSON Object to File.");
            return console.log(err);
        }
     
        console.log("JSON file has been saved.");
    });
}

main();

//stringToHex("F3 0F 10 0D ? ? ? ? F3 0F 11 4C 24 ? 8B 44 24 20 35 ? ? ? ? 89 44 24 0C");


function stringToHex(input)
{
    let ins = input.split(' ');
    let done = "";
    for(var i=0;i<=ins.length-1;i++){
                if(ins[i]=="?")
                    ins[i]= -1;
                else
                ins[i]= parseInt(ins[i], 16);
        };
    return ins;
}