import useFetch from "../hooks/useFetch";

export function getTextRawText(textId){
    return useFetch("/usertext/rawcontent?textId="+textId, "GET");
}

export function deleteText(textId){
    return useFetch("/usertext?textId="+textId, "DELETE");
}

export function getTextSubmittmentInfo(textId){
    
    return;
}

export function getAssignmentTexts(assignmentId){
    return useFetch("/assignment/usertexts?assignmentId="+assignmentId, "GET");
}