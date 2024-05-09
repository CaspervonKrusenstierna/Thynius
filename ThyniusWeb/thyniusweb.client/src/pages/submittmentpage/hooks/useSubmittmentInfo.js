import { useEffect, useState } from "react";
import useFetch from "../../../shared/hooks/useFetch";

export default function useSubmittmentInfo(textId) {
    const [submittmentInfo, setSubmittmentInfo] = useState();
    useEffect(() => {
        useFetch("/usertext?textId="+textId, "GET").then(s => s.json()).then(s => {setSubmittmentInfo(s)});
    }, [])

    return submittmentInfo;
}
