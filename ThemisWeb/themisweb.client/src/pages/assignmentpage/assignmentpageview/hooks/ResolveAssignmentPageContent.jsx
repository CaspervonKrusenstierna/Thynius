import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { AssignmentOverviewPage, AssignmentSubmittmentsPage } from "../pages";

export default function ResolveAssignmentPageContent() {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  useEffect(() => {
    switch(page){
      case "description": setPageContent(<AssignmentOverviewPage></AssignmentOverviewPage>); break;
      case "submittments": setPageContent(<AssignmentSubmittmentsPage></AssignmentSubmittmentsPage>); return;
    }
  }, [page]);
  return pageContent;
}
  