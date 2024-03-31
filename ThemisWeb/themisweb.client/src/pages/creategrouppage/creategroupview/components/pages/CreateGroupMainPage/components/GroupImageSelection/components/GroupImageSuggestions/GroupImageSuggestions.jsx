import React from 'react'
import "./GroupImageSuggestions.css"
import RowedItemsContainer from '../../../../../../../../../../shared/components/roweditemscontainer/RowedItemsContainer'


export const GroupImageSuggestions = () => {
  return (
    <div className='GroupImageSuggestions'>
        <p className='GroupImageSuggestions-Title'>
            Förslag
        </p>
        <RowedItemsContainer></RowedItemsContainer>
    </div>
  )
}
