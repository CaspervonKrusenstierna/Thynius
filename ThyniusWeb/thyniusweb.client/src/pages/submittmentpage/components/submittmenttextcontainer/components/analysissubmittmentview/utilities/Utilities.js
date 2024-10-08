
export async function sleep(msec, sleepsStorage) {
    return new Promise(resolve => {let t = setTimeout(resolve, msec); sleepsStorage?.current.push(t); return t;});
  }
export async function clearSleeps(sleepsStorage){
    for (var i=0; i<sleepsStorage.current.length; i++) {
        clearTimeout(sleepsStorage.current[i]);
      }
}
export function parseCsv(data, fieldSep, newLine) {
    var nSep = '\x1D';
    var qSep = '\x1E';
    var cSep = '\x1F';
    var nSepRe = new RegExp(nSep, 'g');
    var qSepRe = new RegExp(qSep, 'g');
    var cSepRe = new RegExp(cSep, 'g');
    var fieldRe = new RegExp('(?<=(^|[' + fieldSep + '\\n]))"(|[\\s\\S]+?(?<![^"]"))"(?=($|[' + fieldSep + '\\n]))', 'g');
    var grid = [];
    data.replace(/\r/g, '').replace(/\n+$/, '').replace(fieldRe, function(match, p1, p2) {
        return p2.replace(/\n/g, nSep).replace(/""/g, qSep).replace(/,/g, cSep);
    }).split(/\n/).forEach(function(line) {
        var row = line.split(fieldSep).map(function(cell) {
            return cell.replace(nSepRe, newLine).replace(qSepRe, '"').replace(cSepRe, ',');
        });
        grid.push(row);
    });
    return grid;
}

export function ThemisInputsAdvance(rawText, input){
    let toReturn = rawText;
    let content = input[0];
    let type = parseInt(input[1]);
    let selStart = parseInt(input[2]);
    let selEnd = parseInt(input[3]);
    switch (parseInt(input[1]))
    {
        case 0: 
            toReturn = toReturn.slice(0, selStart) + content + toReturn.slice(selStart, toReturn.length)
            break;

        case 1:
            if (selStart == selEnd)
            {
              toReturn = toReturn.slice(0, selStart-1) + toReturn.slice(selStart, toReturn.length);
            }
            else{
              toReturn = toReturn.slice(0, selStart) + toReturn.slice(selEnd, toReturn.length);
            }
            break;

        case 2: 
            if(selStart != selEnd) // REPLACE OPERATION
            {
              toReturn = toReturn.slice(0, selStart) + toReturn.slice(selEnd, toReturn.length);
            }
            toReturn = toReturn.slice(0, selStart) + content + toReturn.slice(selStart, toReturn.length);
            break;
    }
    return toReturn;
}

export function GetThemisInputIndexRawText(startingPointText, inputs, startIndex, length){
    let toReturn = startingPointText;
    let currSession = 0;
    for(let i = 0; length > i; i++){
        if(inputs[i][1] == 8){
            currSession++;
        }
        toReturn = ThemisInputsAdvance(toReturn, inputs[startIndex+i]);
    }
    return {text: toReturn, session: currSession};
}