import React, { useContext } from 'react'
import "./GroupAssignmentsView.css"
import CreateAssignmentButton from './components/CreateAssignmentButton/CreateAssignmentButton';
import AssignmentView from './components/AssignmentView/AssignmentView';
import useDynamicAssignmentsRowCount from './useDynamicAssignmentsRowCount';
import { sessionInfoContext } from '../../../../../App';
import RowedItemsContainer from '../../../../../shared/components/roweditemscontainer/RowedItemsContainer';
import { groupinfoContext } from '../../GroupPageView';

const GroupAssignmentsView = (props) => {
  const assignmentsPerRowCount = useDynamicAssignmentsRowCount();
  const groupInfo = useContext(groupinfoContext);
  const sessionInfo = useContext(sessionInfoContext);

  return (
    <div className='GroupAssignmentsView'>
        {groupInfo?.ManagerData?.Id == sessionInfo?.ID ? <CreateAssignmentButton groupId={groupInfo?.id}></CreateAssignmentButton> : <></>}
        <RowedItemsContainer ItemsPerRow={assignmentsPerRowCount}  Filler={<div className='Assignment-Filler'></div>}
        Items={groupInfo?.assignmentDatas?.map(s => <AssignmentView assignmentId={s.Id} assignmentName={s.Name} groupId={groupInfo?.id}></AssignmentView>)}></RowedItemsContainer>
    </div>
  )
}

export default GroupAssignmentsView