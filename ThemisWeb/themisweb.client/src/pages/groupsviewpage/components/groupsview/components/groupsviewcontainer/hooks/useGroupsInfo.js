import React, { useContext, useEffect, useState } from "react"
import useFetch from "../../../../../../../shared/hooks/useFetch"
import { sessionInfoContext } from "../../../../../../../App"

export default function useGroupsInfo() {
    const [groupsInfo, setGroupsInfo] = useState();
    const sessionInfo = useContext(sessionInfoContext);
    useEffect(() => {
        if(sessionInfo != null){
            useFetch("/groups/getusergroups?userId=" + sessionInfo.ID, "GET").then(i => i.json()).then(i => {console.log(i); setGroupsInfo(i)});
        }
    }, [sessionInfo])
    return groupsInfo
}
