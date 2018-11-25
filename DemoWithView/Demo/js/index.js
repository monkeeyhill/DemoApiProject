(function () {
  "use strict";
  $(document).ready(function(){
    // selector element
    const $btnAdd = $('.btn-add')
    const $modal = $('.modal')
    const $modalContent = $('.modal-content')
    const $btnClose = $('.btn-close')
    const $btnSave = $('.btn-save')
    const $formAddNew = $('#form-add-new')
    const $errorClass = $('.error')
    const $listTodos = $('.list-todos tbody')
    const $notification = $('.notification')
    const $notificationMessage = $('.notification-message')

    //form input
    const $name = $('#name')
    const $description = $('#description')
    const $order = $('#order')
    const $todoId = $('input[name=id]')

    const closePopup = () => {
      $modal.addClass('hidden')
      $formAddNew.validate().resetForm()
      $formAddNew.trigger("reset")
      $('.error').removeClass('error')
      //set id empty
      $todoId.val('')
      $('body').css('overflow', 'auto')
    }

    const openPopup = () => {
      $modal.removeClass('hidden')
      $('body').css('overflow', 'hidden')
    }

    const showMessage = (message = '') => {
      $notificationMessage.text(message)
      $notification.slideToggle('fast')
      setTimeout(function() {
        $notification.slideToggle('fast')
      }, 3000)
    }

    const getListTodos = () => {
      //get data
      $.ajax({
        type: 'GET',
        url: 'http://jissoft.site/api/todos',
        dataType: 'json',
        beforeSend: function() {
          $listTodos.append(renderLoading())
        },
        success: function(data){
          const result = renderRowData(data.Data)
          $listTodos.html('')
          $listTodos.append(result)
        }
      });
    }

    $formAddNew.validate({
      rules: {
        name: 'required',
        description: 'required',
        order: {
          required: true,
          number: true
        }
      },
      messages: {
        name: 'Please enter todo name',
        description: 'Please enter description',
        order: {
          required: 'Please enter order',
          number: 'Please enter a number'
        }
      }
    })
    
    $btnAdd.click(function() {
      $modalContent.find('h3').text('Add todo')
      openPopup()
    })

    $listTodos.delegate('.btn-edit', 'click', function(e) {
      $modalContent.find('h3').text('Edit todo')
      openPopup()
      const cells = $(this).closest('tr')[0].cells
      $todoId.val($(this).attr('data-id'))
      $name.val(cells[1].innerText)
      $description.val(cells[2].innerText)
      $order.val(cells[3].innerText)
    })

    $listTodos.delegate('.btn-delete', 'click', function() {
      if (confirm('Are your sure delete this toto ?')) {
        $.ajax({
          type: 'DELETE',
          url: 'http://jissoft.site/api/todos',
          dataType: 'json',
          data: {
            id: $(this).attr('data-id')
          },
          success: function(data){
            getListTodos()
            showMessage('Todo deleted successfully')
          },
          error: function(error) {
            showMessage('Cannot add todo')
          }
        });
      }
    })

    $btnClose.click(function() {
      closePopup()
    })

    $modal.click(function() {
      closePopup()
    })

    $modalContent.click(function(e) {
      e.stopPropagation()
    })

    $btnSave.click(function() {
      if($formAddNew.valid()) {
        if ($todoId.val()) {
          // update
          const data = {
            id: $todoId.val(),
            name: $name.val().trim(),
            description: $description.val().trim(),
            Ordering: $order.val().trim()
          }
          $.ajax({
            type: 'PUT',
            url: 'http://jissoft.site/api/todos',
            dataType: 'json',
            data: data,
            success: function(data){
              getListTodos()
              showMessage('Todo updated successfully')
            },
            error: function(error) {
              showMessage('Cannot add todo')
            }
          });
        } else {
          // add
          const data = {
            name: $name.val().trim(),
            description: $description.val().trim(),
            Ordering: $order.val().trim()
          }
          $.ajax({
            type: 'POST',
            url: 'http://jissoft.site/api/todos',
            dataType: 'json',
            data: data,
            success: function(data){
              getListTodos()
              showMessage('Todo added successfully')
            },
            error: function(error) {
              showMessage('Cannot add todo')
            }
          });
        }
        closePopup()
      }
    })

    getListTodos()
    
  })

  function renderRowData(data) {
    return data.sort((a, b)=> a.Ordering - b.Ordering).map((item, index) => {
      return `<tr>
        <td class='text-center'>${index + 1}</td>
        <td>${item.Name}</td>
        <td>${item.Description}</td>
        <td class='text-center'>${item.Ordering}</td>
        <td class='text-center'>
          <button class='btn btn-primary btn-edit' data-id='${item.Id}'>Edit</button>
          <button class='btn btn-danger btn-delete' data-id='${item.Id}'>Delete</button>
        </td>
      </tr>`
    })
  }

  function renderLoading() {
    return `<tr>
      <td colspan='5'>Loading...</td>
    </tr>`
  }
})()