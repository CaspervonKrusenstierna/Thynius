import React, { useContext, useEffect, useState } from "react"
import useFetch from "../../../../../../shared/hooks/useFetch";
import { sessionInfoContext } from "../../../../../../App";

export default function useTextsInfo() {
    const [textsInfo, setTextsInfo] = useState();
    const sessionInfo = useContext(sessionInfoContext).sessionInfo;
    useEffect(() => {
        if(sessionInfo != null){
            useFetch("/user/usertexts?userId=" + sessionInfo.ID, "GET").then(i => i.json()).then(i => {setTextsInfo(i)});
        }
    }, [sessionInfo])
    return textsInfo
}
