import { useEffect, useState } from "react";
import useFetch from "../../../../../shared/hooks/useFetch";

export default function useExtendedGroupInfo(groupId) {
    const [groupInfo, setGroupInfo] = useState();
    useEffect(() => {
        useFetch("/group/getgroupinfo?groupId=" + groupId, "GET").then(s => s.json()).then(s => {console.log(s); setGroupInfo(s)});
    }, [])
    return groupInfo;
}
