import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ManageGroupMembersPage from "../components/dashboard/managegroupmemberspage/ManageGroupMembersPage";
import ManageGroupMainPage from "../components/dashboard/managegroupmainpage/ManageGroupMainPage";

export default function useResolveCreateGroupContent(groupInfo) {
    let { page } = useParams();
    const [content, setContent] = useState(<></>);

    useEffect(() =>{
        switch(page){
          case "general": setContent(<ManageGroupMainPage groupInfo={groupInfo}></ManageGroupMainPage>); break;
          case "members": setContent(<ManageGroupMembersPage groupInfo={groupInfo}></ManageGroupMembersPage>); break;
        }
      }, [page])
      return content;
}
