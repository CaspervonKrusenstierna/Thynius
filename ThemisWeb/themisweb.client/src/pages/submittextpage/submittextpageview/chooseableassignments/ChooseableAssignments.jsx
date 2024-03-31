import React from 'react'
import useChooseableAssignments from './hooks/useChooseableAssignments'
import useDynamicGroupsRowCount from '../../../groupsviewpage/components/groupsview/components/groupsviewcontainer/hooks/useDynamicGroupsRowCount';
import RowedItemsContainer from '../../../../shared/components/roweditemscontainer/RowedItemsContainer';
import ChooseableAssignment from './chooseableassignment/ChooseableAssignment';
import "./ChooseableAssignments.css"


const ChooseableAssignments = (props) => {
  const chooseableAssignments = useChooseableAssignments();
  const rowCount = useDynamicGroupsRowCount(); // can use same here
  return (
    <div className=''>
        <RowedItemsContainer ItemsPerRow={rowCount} Items={chooseableAssignments?.map(j => <ChooseableAssignment onClick={(id) => {props.onSelectAssignment(id)}} name={j.Name} Id={j.Id}></ChooseableAssignment>)} Filler={<div className="Filler"></div>}></RowedItemsContainer>
    </div>
  )
}

export default ChooseableAssignments