function createItem(templateSelector, appendingBlockSelector)
{
    var emptyForm = $( templateSelector ).last().clone();
    emptyForm.css( "display", "block" );
    $( emptyForm.children()[0] ).children().each( function ( z, k )
    {
        $( k ).removeAttr( "disabled" );
    } );

    $( appendingBlockSelector ).append( emptyForm );
}

function removeItem( element )
{
    var nodeToDelete = element.parentNode;
    var nodeDeleteFrom = element.parentNode.parentNode;
    nodeDeleteFrom.removeChild( nodeToDelete );
}