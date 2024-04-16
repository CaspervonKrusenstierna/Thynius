import React, { useContext, useEffect, useState } from "react"
import useFetch from "../../../../../../../shared/hooks/useFetch"
import { sessionInfoContext } from "../../../../../../../App"
import { sleep } from "../../../../../../submittmentpage/components/submittmenttextcontainer/components/analysissubmittmentview/utilities/Utilities";

export default function useGroupsInfo() {
    const [groupsInfo, setGroupsInfo] = useState();
    const sessionInfo = useContext(sessionInfoContext).sessionInfo;
    useEffect(() => {
        if(sessionInfo != null){
            useFetch("/group/getusergroups?userId=" + sessionInfo.ID, "GET").then(i => i.json()).then(i => {sleep(10000000000000000000000);setGroupsInfo(i)});
        }
    }, [sessionInfo])
    return [groupsInfo, setGroupsInfo];
}
