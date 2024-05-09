import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { GroupAssignmentsView, GroupHomeView } from "../pages";
import GroupManageAssignmentView from "../pages/groupmanageassignmentview/GroupManageAssignmentView";
import { createAssignment, editAssignment, getAssignmentinfo } from "../../../../shared/network/assignment";
import { getManageAssignmentErrors } from "../pages/groupmanageassignmentview/useManageAssignmentErrors";



export default function ResolveGroupContent(groupInfoTouched, setGroupInfoTouched) {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  const { id, assignmentid } = useParams();
  const initialImage = useRef();
  const navigate = useNavigate();
  const [submitCount, setSubmitCount] = useState(0);

  async function onCreateAssignment(state){
    if(getManageAssignmentErrors(state.description, state.name, state.date, state.image) != null){
      setSubmitCount(submitCount + 1)
      return;
    }
    let response = await createAssignment(id, state.name, state.description, state.date, state.image);
    if(response.ok){
      setGroupInfoTouched(!groupInfoTouched);
      navigate(-1);
    }
  }

  async function onEditAssignment(state){
    if(getManageAssignmentErrors(state.description, state.name, state.date, state.image) != null){
      setSubmitCount(submitCount + 1)
      return;
    }

    let response;
    if(initialImage.current == state.image){
      response = await editAssignment(assignmentid, state.name, state.description, state.date, null)
    }
    else{
      response = await editAssignment(assignmentid, state.name, state.description, state.date, state.image)
    }
    if(response.ok){
      setGroupInfoTouched(!groupInfoTouched);
      navigate(-1);
    }
  }

  useEffect(() => {
    switch(page){
      case "Home":
        setPageContent(<GroupHomeView></GroupHomeView>); break;
        
      case "Assignments":
        setPageContent(<GroupAssignmentsView></GroupAssignmentsView>); break;

      case "CreateAssignment":
        setPageContent(<GroupManageAssignmentView hasClickedSubmit={submitCount >= 1} onSubmit={onCreateAssignment}></GroupManageAssignmentView>); break;

      case "EditAssignment":
           getAssignmentinfo(assignmentid).then(s => s.json()).then(
            s => {initialImage.current = s.imageURL; setPageContent(<GroupManageAssignmentView hasClickedSubmit={submitCount >= 1} onSubmit={onEditAssignment} initialState={{name: s.Name, description: s.Description, date: s.DueDate, image: s.imageURL}}></GroupManageAssignmentView>)});
            break;
    }
  }, [page, submitCount]);
  return pageContent;
}
