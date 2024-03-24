import React, { useEffect, useState } from "react";

export default function useDynamicAssignmentsRowCount() {
    const [itemsPerRow, setItemsPerRow] = useState(0);
    function onResize(){
      let width = window.innerWidth;
      if(width >= 1100){
        setItemsPerRow(3);
      }
      else if(width >= 800){
        setItemsPerRow(2)
      }
      else{
        setItemsPerRow(1);
      }
    }

    useEffect(() => {
        onResize();
        window.addEventListener("resize", onResize);
    }, []);

    return itemsPerRow;
}
