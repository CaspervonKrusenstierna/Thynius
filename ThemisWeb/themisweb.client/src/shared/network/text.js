import useFetch from "../hooks/useFetch";

export function getTextRawText(textId){
    return useFetch("/usertext/rawcontent?textId="+textId, "GET");
}