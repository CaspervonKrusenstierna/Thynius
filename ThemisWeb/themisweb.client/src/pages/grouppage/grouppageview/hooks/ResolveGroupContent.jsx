import { useContext, useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { GroupAssignmentsView, GroupHomeView } from "../pages";
import GroupManageAssignmentView from "../pages/groupmanageassignmentview/GroupManageAssignmentView";
import { createAssignment, editAssignment, getAssignmentinfo } from "../../../../shared/network/assignment";
import { groupinfoContext } from "../GroupPageView";



export default function ResolveGroupContent(groupInfoTouched, setGroupInfoTouched) {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  const { id, assignmentid } = useParams();
  const initialImage = useRef();
  const navigate = useNavigate();

  async function onCreateAssignment(ref, state){
    let response = await createAssignment(id, ref.name, ref.description, state.date, state.image);
    if(response.ok){
      setGroupInfoTouched(!groupInfoTouched);
      navigate(-1);
    }
  }
  async function onEditAssignment(ref, state){
    let response;
    if(initialImage.current == state.image){
      response = await editAssignment(assignmentid, ref.name, ref.description, state.date, null)
    }
    else{
      response = await editAssignment(assignmentid, ref.name, ref.description, state.date, state.image)
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
        setPageContent(<GroupManageAssignmentView onSubmit={onCreateAssignment}></GroupManageAssignmentView>); break;

      case "EditAssignment":
           getAssignmentinfo(assignmentid).then(s => s.json()).then(
            s => {initialImage.current = s.imageURL; setPageContent(<GroupManageAssignmentView onSubmit={onEditAssignment} initialRef={{name: s.Name, description: s.Description}} initialState={{date: s.DueDate, image: s.imageURL}}></GroupManageAssignmentView>)});
            break;
    }
  }, [page]);
  return pageContent;
}
