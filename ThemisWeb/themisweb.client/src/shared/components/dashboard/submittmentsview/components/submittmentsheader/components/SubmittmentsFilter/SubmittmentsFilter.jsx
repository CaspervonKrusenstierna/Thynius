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
        <Select onValueChange={(i) => {props.setFilter(i)}}>
            <SelectTrigger className="w-[180px]">
            <SelectValue placeholder="Visa" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value={0}>Visa alla</SelectItem>
              <SelectItem value={1}>Inga tecken på fusk</SelectItem>
              <SelectItem value={2}>Tecken på fusk</SelectItem>
              <SelectItem value={3}>Stora tecken på fusk</SelectItem>
            </SelectContent>
        </Select>
    )
  }
  
  export default SubmittmentsFilter