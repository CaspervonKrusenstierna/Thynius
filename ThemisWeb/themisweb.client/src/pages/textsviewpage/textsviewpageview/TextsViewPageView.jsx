import React from 'react'
import "./TextsViewPageView.css"
import useTextsInfo from './hooks/useTextsInfo'
import useDynamicTextsRowCount from './hooks/useDynamicTextsRowCount';
import TextView from './components/textview/TextView';
import RowedItemsContainer from '../../../shared/components/roweditemscontainer/RowedItemsContainer';
const TextsViewPageView = () => {
  const textsInfo = useTextsInfo();
  const textsRowCount = useDynamicTextsRowCount();

  return (
    <div className='TextsViewPageView'>
      <div className='TextsContainer'>
      {<RowedItemsContainer ItemsPerRow={textsRowCount} Items={textsInfo?.map(j =><TextView Id={j.Id} Title={j.Title}></TextView>)} Filler={<div className="TextFiller"></div>}></RowedItemsContainer>}
      </div>
    </div>
  )
}

export default TextsViewPageView