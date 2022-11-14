Include = {"Math", "Vector", "Time"}

function Update (Entity)

    local rigidbody = Entity.GetComponentByID("Rigidbody"); 
    -- rigidbody.AddForce(Vector.up * 500);
    rigidbody.velocity = Vector.up * 10;
    -- print(health);
end
