import useFetch from "../hooks/useFetch";

export async function createAssignment(groupId, name, description, dueDate, image){
    let formData = new FormData();
    formData.append("groupId", groupId);
    formData.append("name", name);
    formData.append("description", description);
    formData.append("dueDate", dueDate.toJSON());
    let blob = await fetch(image).then(r => r.blob());
    formData.append("image", blob);
    return fetch("/assignment", {
        method: "POST",
        body: formData
    });
}

export async function getAssignmentinfo(assignmentId){
    return useFetch("/assignment?assignmentId="+assignmentId, "GET")
}

export async function deleteAssignment(assignmentId){
    return useFetch("/assignment?assignmentId="+assignmentId, "DELETE");
}

export async function editAssignment(assignmentId, name, description, dueDate, image){
    let formData = new FormData();
    formData.append("assignmentId", assignmentId);
    formData.append("name", name);
    formData.append("description", description);
    try{
        formData.append("dueDate", dueDate.toJSON());
    }
    catch{
        formData.append("dueDate", dueDate);
    }
    if(image){
        let blob = await fetch(image).then(r => r.blob());
        formData.append("image", blob);
    }
    return fetch("/assignment", {
        method: "PUT",
        body: formData
    });
}