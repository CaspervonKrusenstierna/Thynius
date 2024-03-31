import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GroupAssignmentsView, GroupCreateAssignmentView, GroupHomeView } from "../pages";


export default function ResolveGroupContent() {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  useEffect(() => {
    switch(page){
      case "Home":
        setPageContent(<GroupHomeView></GroupHomeView>); break;
        
      case "Assignments":
        setPageContent(<GroupAssignmentsView></GroupAssignmentsView>); break;

      case "CreateAssignment":
        setPageContent(<GroupCreateAssignmentView></GroupCreateAssignmentView>); break;
    }
  }, [page]);
  return pageContent;
}
