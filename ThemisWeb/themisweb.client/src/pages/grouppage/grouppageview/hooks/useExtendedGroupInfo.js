import { useEffect, useState } from "react";
import useFetch from "../../../../shared/hooks/useFetch";


export default function useExtendedGroupInfo(groupId, groupInfoTouched) {
    const [groupInfo, setGroupInfo] = useState();
    useEffect(() => {
        useFetch("/group?groupId=" + groupId, "GET").then(s => s.json()).then(s => {setGroupInfo(s)});
    }, [groupInfoTouched])
    return groupInfo;
}
