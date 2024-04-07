import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { GroupAssignmentsView, GroupHomeView } from "../pages";
import GroupManageAssignmentView from "../pages/groupmanageassignmentview/GroupManageAssignmentView";
import { createAssignment, editAssignment, getAssignmentinfo } from "../../../../shared/network/assignment";



export default function ResolveGroupContent() {
  const { page } = useParams();
  const [pageContent, setPageContent] = useState(<></>);
  const { id, assignmentid } = useParams();
  const initialImage = useRef();

  function onCreateAssignment(ref, state){
    createAssignment(id, ref.name, ref.description, state.date, state.image);
  }
  function onEditAssignment(ref, state){
    if(initialImage.current == state.image){
      editAssignment(assignmentid, ref.name, ref.description, state.date, null)
    }
    else{
      editAssignment(assignmentid, ref.name, ref.description, state.date, state.image)
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
