import React, { useEffect, useState } from 'react'
import "./RowedItemsContainer.css"

const RowedItemsContainer = (props) => {
  const [rows, setRows] = useState([[]]);
  useEffect(() => {
    let count = 0;
    let currRow = [];
    let temp = [];
    for(let i = 0; props.Items?.length > i; i++){
      currRow.push(props.Items[i]);
      if(count == props.ItemsPerRow-1 || i ==  props.Items.length-1){
          for(let j = 0; props.ItemsPerRow-1-count > j; j++){
            currRow.push("Filler")
          }


        temp.push(currRow);
        count = 0;
        currRow = [];
        continue;
      }
      count++;
    }
    console.log(temp);
    setRows(temp);
  }, [props.Items])

  return (
    <div className='RowedItemsContainer'>
      {rows.map(i => {return <div className='RowedItemsContainer-Row'>{i.map(j => j=="Filler" ? props.Filler : j)}</div>})}
    </div>);
}

export default RowedItemsContainer