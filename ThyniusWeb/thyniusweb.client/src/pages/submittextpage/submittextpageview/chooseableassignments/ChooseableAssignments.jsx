import React from 'react'
import useChooseableAssignments from './hooks/useChooseableAssignments'
import useDynamicGroupsRowCount from '../../../groupsviewpage/components/groupsview/components/groupsviewcontainer/hooks/useDynamicGroupsRowCount';
import ChooseableAssignment from './chooseableassignment/ChooseableAssignment';
import "./ChooseableAssignments.css"
import { RowedItemsContainer } from '../../../../shared/components/dashboard';
import getGroupSkeletonItems from '../../../groupsviewpage/components/groupsview/components/groupsviewcontainer/utils/getGroupSkeletonItems';

const ChooseableAssignments = (props) => {
  const chooseableAssignments = useChooseableAssignments();
  const rowCount = useDynamicGroupsRowCount(); // can use same here
  return (
    <>
        {chooseableAssignments ? 
        <RowedItemsContainer ItemsPerRow={rowCount} Items={chooseableAssignments?.map(j => <ChooseableAssignment onClick={(id) => {props.onSelectAssignment(id)}} name={j.Name} Id={j.Id}></ChooseableAssignment>)} Filler={<div className="Filler"></div>}></RowedItemsContainer>
         :
         <RowedItemsContainer ItemsPerRow={rowCount} Items={getGroupSkeletonItems(rowCount)}></RowedItemsContainer> // just use same here
         }
    </>
  )
}

export default ChooseableAssignments