import React, { useEffect, useState } from 'react'
import "./GroupMainContent.css"
import GroupHomeView from './components/grouphomeview/GroupHomeView';
import GroupAssignmentsView from './components/groupassignmentsview/GroupAssignmentsView';
import GroupCreateAssignmentView from './components/groupcreateassignmentview/GroupCreateAssignmentView';

const GroupMainContent = (props) => {
  const [pageContent, setPageContent] = useState(<></>);
  useEffect(() => {
    switch(props.Page){
      case "Home":
        setPageContent(<GroupHomeView groupInfo={props.groupInfo}></GroupHomeView>); break;
        
      case "Assignments":
        setPageContent(<GroupAssignmentsView groupInfo={props.groupInfo}></GroupAssignmentsView>); break;

      case "CreateAssignment":
        setPageContent(<GroupCreateAssignmentView groupInfo={props.groupInfo}></GroupCreateAssignmentView>); break;
    }
  }, [props.Page, props.groupInfo]);
  return (
    <div className='GroupMainContent'>{pageContent}</div>
  )
}

export default GroupMainContent