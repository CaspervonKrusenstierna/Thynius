import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CreateGroupMainPage, CreateGroupMembersPage } from "../components/pages";

export default function ResolveCreateGroupContent() {
    let { page } = useParams();
    const [content, setContent] = useState(<></>);

    useEffect(() =>{
        switch(page){
          case "general": setContent(<CreateGroupMainPage className="CreateGroupViewPage"></CreateGroupMainPage>); break;
          case "members": setContent(<CreateGroupMembersPage></CreateGroupMembersPage>); break;
        }
      }, [page])
      return content;
}
