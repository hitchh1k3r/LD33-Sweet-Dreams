<?php

  // PLEASE SEND SECURITY CONCERNS TO hitch@hitchh1k3rsguide.com
  function sanitize($database, $str, $len_limit = false)
  {
    if($len_limit)
    {
      return substr(str_replace('~', '', $database->escapeString($str)), 0, 32);
    }
    else
    {
      return str_replace('~', '', $database->escapeString($str));
    }
  }

  if(isset($_POST['NAME']) && isset($_POST['AUTHOR']) && isset($_POST['DATA']))
  {
    require_once('database.php');
    if($database != null)
    {
      $database->query("INSERT INTO 'shadows' ('ID', 'name', 'author', 'data') VALUES (NULL, '" . sanitize($database, $_POST['NAME'], true) . "', '" . sanitize($database, $_POST['AUTHOR'], true) . "', '" . sanitize($database, $_POST['DATA']) . "');");
    }
  }

?>