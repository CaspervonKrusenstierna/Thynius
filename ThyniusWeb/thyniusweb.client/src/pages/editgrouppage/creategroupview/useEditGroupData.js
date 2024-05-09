import { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import { getGroupMembers } from "../../../shared/network/group";


export default function useEditGroupData(id) {
    const location = useLocation();
    const [groupData, setGroupData] = useState({groupMembers: location.state.members ? location.state.members : null, currentMembersPage: 1, groupName: location.state.name, groupImg: location.state.img, nameError: false, pictureError: false});
    const [loading, setLoading] = useState(true);
    
    useEffect(() => {
        if(groupData.groupMembers != null){
            setLoading(false)
        }else{
            getGroupMembers(id).then(s => s.json()).then(s => {let temp = groupData; temp.groupMembers=s; setGroupData(temp); setLoading(false)})
        }
    }, [])

    return [groupData, loading];
}
