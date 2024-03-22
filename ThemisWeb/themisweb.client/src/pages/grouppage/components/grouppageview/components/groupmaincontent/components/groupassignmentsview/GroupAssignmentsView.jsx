import React, { useContext } from 'react'
import "./GroupAssignmentsView.css"
import { sessionInfoContext } from '../../../../../../../../App';
import CreateAssignmentButton from './components/CreateAssignmentButton/CreateAssignmentButton';
import RowedItemsContainer from '../../../../../../../../shared/components/roweditemscontainer/RowedItemsContainer';
import AssignmentView from './components/AssignmentView/AssignmentView';

const GroupAssignmentsView = (props) => {
  const SessionInfo = useContext(sessionInfoContext);
  return (
    <div className='GroupAssignmentsView'>
        {props.Manager?.Id == SessionInfo?.Id ? <CreateAssignmentButton groupId={props.groupInfo?.Id}></CreateAssignmentButton> : <></>}
        <RowedItemsContainer ItemsPerRow={3}  Filler={<div className='Filler'></div>}
        Items={props.groupInfo?.assignmentDatas?.map(s => <AssignmentView assignmentId={s.Id} assignmentName={s.Name} groupId={props.groupInfo?.Id}></AssignmentView>)}></RowedItemsContainer>
    </div>
  )
}

export default GroupAssignmentsView