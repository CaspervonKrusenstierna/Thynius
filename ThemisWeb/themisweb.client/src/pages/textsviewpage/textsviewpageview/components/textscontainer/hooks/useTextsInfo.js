import React, { useContext, useEffect, useState } from "react"
import useFetch from "../../../../../../shared/hooks/useFetch";
import { sessionInfoContext } from "../../../../../../App";
import { sleep } from "../../../../../submittmentpage/components/submittmenttextcontainer/components/analysissubmittmentview/utilities/Utilities";

export default function useTextsInfo() {
    const [textsInfo, setTextsInfo] = useState();
    const sessionInfo = useContext(sessionInfoContext).sessionInfo;
    useEffect(() => {
        if(sessionInfo != null){
            useFetch("/user/usertexts?userId=" + sessionInfo.ID, "GET").then(i => i.json()).then(async i => {setTextsInfo(i)});
        }
    }, [sessionInfo])
    return textsInfo
}
