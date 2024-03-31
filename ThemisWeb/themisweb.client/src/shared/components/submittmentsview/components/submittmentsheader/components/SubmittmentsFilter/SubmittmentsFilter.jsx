import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
  } from "@/components/ui/select"
  import React from 'react'
  
  const SubmittmentsFilter = (props) => {
    return (
        <Select>
            <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Filtrera" />
            </SelectTrigger>
            <SelectContent>
            <SelectItem value="1" onSelect={props.setFilter(0)}>Inlämnad</SelectItem>
            <SelectItem value="2" onSelect={props.setFilter(1)}>Ej Inlämnad</SelectItem>
            <SelectItem value="3" onSelect={props.setFilter(2)}>Flaggad</SelectItem>
            </SelectContent>
        </Select>
    )
  }
  
  export default SubmittmentsFilter