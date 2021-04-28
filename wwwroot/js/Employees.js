function onClickSubmit() {
    let nume = document.getElementById("numeAngajat").value;
    let lastName = document.getElementById("prenumeAngajat").value;
    let sex = document.getElementById("emailAngajat").value;
    let email = document.getElementById("genAngajat").value;
    let dataNastere = document.getElementById("dataN").value;

    let tableContent = `
                      <tr>
                          <td>${nume}</td>
                          <td>${lastName}</td>
                          <td>${sex}</td>
                          <td>${email}</td>
                          <td>${dataNastere}</td>
                          <td><input type="button" value="Delete" onclick="deleteRow(this)"></td>
                      </tr>
                          `;

    document.getElementById("angajatiT").innerHTML += tableContent;
}

function deleteRow(r) {
    var i = r.parentNode.parentNode.rowIndex;
    document.getElementById("angajatiT").deleteRow(i);
}

function searchEmployee() {
    // Declare variables
    var input, filter, table, tr, td, i, txtValue;
    input = document.getElementById("searchBar");
    filter = input.value.toUpperCase();
    table = document.getElementById("angajatiT");
    tr = table.getElementsByTagName("tr");

    // Loop through all table rows, and hide those who don't match the search query
    for (i = 0; i < tr.length; i++) {
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    }
}