import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { AssignmentOverviewPage, AssignmentSubmittmentsPage } from "../pages";
import AssignmentNotSubmittedPage from "../pages/AssignmentNotSubmittedPage/AssignmentNotSubmittedPage";

export default function ResolveAssignmentPageContent() {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  useEffect(() => {
    switch(page){
      case "description": setPageContent(<AssignmentOverviewPage></AssignmentOverviewPage>); break;
      case "submittments": setPageContent(<AssignmentSubmittmentsPage></AssignmentSubmittmentsPage>); return;
      case "notsubmitted": setPageContent(<AssignmentNotSubmittedPage></AssignmentNotSubmittedPage>); return;
    }
  }, [page]);
  return pageContent;
}
  